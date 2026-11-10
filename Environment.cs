using System;
using System.Drawing;
using System.Windows.Forms;

namespace KG_Task4
{
    internal class Environment
    {
        private static Environment _instance;
        public static Environment Instance 
        { 
            get
            {
                if (_instance == null)
                    throw new Exception("Environment wasn't initialized.");

                return _instance;
            } 
        }

        public event Action<float> BeforeFrameUpdate;

        public event Action AfterFrameUpdate;


        private Timer _timer;
        private int _lastTickMs;
        private float _deltaTime;

        private SceneDrawer _drawer;

        public Camera Camera => _drawer.Camera;

        private Environment()
        {
            _drawer = new SceneDrawer();

            _timer = new Timer();
            _timer.Interval = 1000 / 30;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        public static void Initialize()
        {
            _instance = new Environment();
        }

        public Bitmap CreateBitmap(int witdh, int height)
        {
            return _drawer.InitializeMap(witdh, height);
        }

        public void SetScene(Scene scene)
        {
            _drawer.Scene = scene;
        }

        private void FrameUpdate()
        {
            BeforeFrameUpdate?.Invoke(_deltaTime);

            _drawer.Update();

            AfterFrameUpdate?.Invoke();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            int delatMs;
            if (DateTime.UtcNow.Millisecond >= _lastTickMs)
                delatMs = DateTime.UtcNow.Millisecond - _lastTickMs;
            else
                delatMs = 1000 - _lastTickMs + DateTime.UtcNow.Millisecond;

            _deltaTime = delatMs / 1000f;
            _lastTickMs = DateTime.UtcNow.Millisecond;

            FrameUpdate();
        }
    }
}
