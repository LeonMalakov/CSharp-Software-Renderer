using KG_Task4.UI;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using KG_Task4.Structures;

namespace KG_Task4
{
    public partial class Form1 : Form
    {
        private Type[] _componentsTypes;
        private string[] _objectsNames;

        private Scene _scene;

        private int _selectedObject;

        private const float MouseSensitivity = 0.3f;
        private const float MoveSpeed = 15f;
        private bool _isRMBPressed;
        private Point _lastMouseLocation;
        private Vector3 _cameraRot;
        private Vector3 _moveInput;

        private Camera _camera;

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;

            SetupComponentsBox();
            SetupObjectsBox();

            Environment.Initialize();
            pictureBox.Image = Environment.Instance.CreateBitmap(pictureBox.Width, pictureBox.Height);
            Environment.Instance.AfterFrameUpdate += () => pictureBox.Refresh();
            Environment.Instance.BeforeFrameUpdate += BeforeFrameUpdate;
            _camera = Environment.Instance.Camera;

            _scene = new Scene();
            _scene.ListModified += UpdateHierarchy;
            _scene.ClearColor = Color.FromArgb(246, 246, 234);

            Environment.Instance.SetScene(_scene);

            _camera.Position = new Vector3(0,0,-10);

            SpawnStartObjects();
        }

        private void BeforeFrameUpdate(float delta)
        {
            _camera.Rotation = Quaternion.FromEuler(_cameraRot);
            _camera.Position += _moveInput * MoveSpeed * delta;
        }

        private void SpawnStartObjects()
        {
            SceneObject quad = new SceneObject("Quad", ObjectsLibrary.LoadObject("Quad"));
            quad.Transform.Position = new Vector3(5, 3, 2);
            quad.Transform.Scale = new Vector3(1, 1, 1);
            quad.Transform.Euler = new Vector3(90, 0, 0);
            quad.AddComponent<Rotator>().RotationEuler = new Vector3(45, 45, 45);

            SceneObject cube = new SceneObject("Cube", ObjectsLibrary.LoadObject("Cube"));
            cube.Transform.Position = new Vector3(-12, -9, 4);
            cube.Transform.Euler = new Vector3(30, 0, 0);
            cube.Transform.Scale = new Vector3(1, 1, 1);

            cube.AddComponent<Rotator>().RotationEuler = new Vector3(0, 0, 25);

            SceneObject icos = new SceneObject("Icosahedron", ObjectsLibrary.LoadObject("Icosahedron"));
            icos.Transform.Position = new Vector3(0, 0, 0);
            icos.Transform.Euler = new Vector3(30, 0, 0);
            icos.Transform.Scale = new Vector3(2, 2, 2);

            icos.AddComponent<Rotator>().RotationEuler = new Vector3(0, 45, 0);

            _scene.AddObject(quad);
            _scene.AddObject(cube);
            _scene.AddObject(icos);
        }

        #region UI
        private void SetupComponentsBox()
        {
            componentsBox.Items.Clear();

            Type compType = typeof(Component);
            _componentsTypes = compType.Assembly.GetTypes().Where((t) => compType.IsAssignableFrom(t) && !t.IsAbstract).ToArray();

            foreach (Type t in _componentsTypes)
                componentsBox.Items.Add(t.Name);
        }

        private void SetupObjectsBox()
        {
            objectsBox.Items.Clear();

            _objectsNames = ObjectsLibrary.GetObjectsNames();

            foreach (string s in _objectsNames)
                objectsBox.Items.Add(s);
        }

        private void AddComponentToSelectedObject()
        {
            if (_selectedObject == -1 || componentsBox.SelectedIndex == -1) return;

            SceneObject obj = _scene.Objects[_selectedObject];

            obj.AddComponent(_componentsTypes[componentsBox.SelectedIndex]);

            UpdateEditor();
        }

        private void SpawnObject()
        {
            if (objectsBox.SelectedIndex == -1) return;

            Mesh m = ObjectsLibrary.LoadObject(_objectsNames[objectsBox.SelectedIndex]);

            SceneObject obj = new SceneObject(_objectsNames[objectsBox.SelectedIndex], m);
            obj.Transform.Position = new Vector3(0, -1.5f, 4);
            obj.Transform.Scale = new Vector3(1, 1, 1);

            _scene.AddObject(obj);
        }

        private void UpdateHierarchy()
        {
            _selectedObject = -1;
            hierarchyBox.Items.Clear();

            foreach (SceneObject so in _scene.Objects)
            {
                hierarchyBox.Items.Add(so.Name);
            }

            UpdateEditor();
        }

        private void UpdateEditor()
        {
            if(_selectedObject == -1)
            {
                editorBox.Controls.Clear();
                return;
            }

            SceneObject obj = _scene.Objects[_selectedObject];

            editorBox.Controls.Clear();

            // TRANFSFORMATION.
            GroupBox transformGb = CreateEditorSection("Transformation", (o, e) => _scene.RemoveObject(obj));
            transformGb.Height = 55 * 3 + 30;

            Vector3Field.Create(transformGb, 30, "Position", obj.Transform.Position, (v) => { obj.Transform.Position = v; });
            Vector3Field.Create(transformGb, 85, "Euler", obj.Transform.Euler, (v) => { obj.Transform.Euler = v; });
            Vector3Field.Create(transformGb, 140, "Scale", obj.Transform.Scale, (v) => { obj.Transform.Scale = v; });

            editorBox.Controls.Add(transformGb);
            //

            // OTHER COMPONENTS.
            foreach (Component c in obj.GetAllComponents())
                editorBox.Controls.Add(CreateEditorForComponent(obj, c));
        }
        #endregion

        #region Events
        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            pictureBox.Image = Environment.Instance.CreateBitmap(pictureBox.Width, pictureBox.Height);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Text = $"({e.Location.X}, {e.Location.Y})";

            if (_isRMBPressed)
            {
                _cameraRot.Y += (_lastMouseLocation.X - e.Location.X) * MouseSensitivity;
                _cameraRot.X += (e.Location.Y - _lastMouseLocation.Y) * MouseSensitivity;
                _cameraRot.X = Utilities.Clamp(_cameraRot.X, -90, 90);
            }

            _lastMouseLocation = e.Location;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
                _isRMBPressed = true;     
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                _isRMBPressed = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                _moveInput.Z = 1;
            else if (e.KeyCode == Keys.S)
                _moveInput.Z = -1;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S)
                _moveInput.Z = 0;
        }

        private void hierarchyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedObject = hierarchyBox.SelectedIndex;
            UpdateEditor();
        }

        private void btnAddComponent_Click(object sender, EventArgs e)
        {
            AddComponentToSelectedObject();
        }

        private void btnSpawnObject_Click(object sender, EventArgs e)
        {
            SpawnObject();
        }
        #endregion

        private GroupBox CreateEditorSection(string header, EventHandler xClick)
        {
            GroupBox gb = new GroupBox();
            gb.Text = header;
            gb.Width = editorBox.Width - 10;

            gb.Height = 30;

            Button btn = new Button();
            btn.Text = "x";
            btn.Size = new System.Drawing.Size(30, 30);
            btn.Location = new System.Drawing.Point(gb.Width - 30, 0);
            btn.Click += xClick;
            gb.Controls.Add(btn);

            return gb;
        }

        private GroupBox CreateEditorForComponent(SceneObject obj, Component comp)
        {
            GroupBox gb = CreateEditorSection(comp.GetType().Name, (s, e) => { obj.RemoveComponent(comp); UpdateEditor(); });

            FieldInfo[] fields = comp.GetType().GetFields();
            int fieldsCount = 0;

            foreach (FieldInfo fi in fields)
            {
                if (fi.FieldType == typeof(Vector3))
                {
                    gb.Height += 55;
                    Vector3Field.Create(gb, 30 + fieldsCount * 55, fi.Name, (Vector3)fi.GetValue(comp),
                        (v) => fi.SetValue(comp, v));

                    fieldsCount++;
                }
            }

            return gb;
        }
    }
}
