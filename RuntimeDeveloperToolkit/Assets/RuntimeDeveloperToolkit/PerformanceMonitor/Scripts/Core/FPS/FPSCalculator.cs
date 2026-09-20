namespace RuntimePerformanceMonitor
{
    public class FPSCalculator
    {
        // FPS Data
        private FPSSnapshot curFPSSnapshot;

        // FPS Calc Rate
        private FPSRate curFpsCalcRate;
        private float fpsCalcDelay = 0.0f;
        
        // Accumulated Frame Data
        private float accumulatedFrameTime;
        private int accumulatedFrames;

        #region Constructor
        
        internal FPSCalculator()
        {
            
        }
        
        #endregion
        
        #region SetUp
        
        internal void SetUp(FPSRate fpsCalcRate)
        {
            curFpsCalcRate = fpsCalcRate;
            accumulatedFrameTime = 0.0f;
            accumulatedFrames = 0;

            curFPSSnapshot = new FPSSnapshot();
            curFPSSnapshot.FPS = 0.0f;
            curFPSSnapshot.FrameTime = 0.0f;
            
            switch (curFpsCalcRate)
            {
                case FPSRate.EveryFrame:
                    fpsCalcDelay = 0.0f;
                    break;
                
                case FPSRate.Hz_1:
                    fpsCalcDelay = 1.0f;
                    break;

                case FPSRate.Hz_5:
                    fpsCalcDelay = 1.0f / 5.0f;
                    break;

                case FPSRate.Hz_10:
                    fpsCalcDelay = 1.0f / 10.0f;
                    break;

                case FPSRate.Hz_25:
                    fpsCalcDelay = 1.0f / 25.0f;
                    break;

                case FPSRate.Hz_60:
                    fpsCalcDelay = 1.0f / 60.0f;
                    break;
            }
        }

        #endregion
        
        #region Calculation
        
        internal (FPSSnapshot, bool) CalcFPS(float deltaTime)
        {
            if (curFpsCalcRate == FPSRate.EveryFrame)
            {
                float frameTime = deltaTime;
                if (frameTime <= 0.0f)
                {
                    return (curFPSSnapshot, false);
                }
                
                float fps = 1.0f / frameTime;
                
                FPSSnapshot fpsSnapshot = new FPSSnapshot
                {
                    FPS = fps,
                    FrameTime = frameTime
                };

                curFPSSnapshot = fpsSnapshot;
                return (curFPSSnapshot, true);
            }
            else
            {
                float frameTime = deltaTime;
                accumulatedFrameTime += frameTime;
                accumulatedFrames++;

                if (accumulatedFrameTime < fpsCalcDelay)
                {
                    return (curFPSSnapshot, false);
                }
                
                if (accumulatedFrameTime <= 0.0f || accumulatedFrames <= 0) 
                {
                    return (curFPSSnapshot, false);
                }
                    
                FPSSnapshot fpsSnapshot = new FPSSnapshot
                {
                    FPS = accumulatedFrames / accumulatedFrameTime,
                    FrameTime = accumulatedFrameTime / accumulatedFrames
                };
                    
                accumulatedFrameTime = 0.0f;
                accumulatedFrames = 0; 
                    
                curFPSSnapshot = fpsSnapshot;
                return (curFPSSnapshot, true);
            }
        }
        
        #endregion
        
        #region Getter
        
        internal float GetFpsCalcDelay() => fpsCalcDelay;
        
        #endregion
    }
}