using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class FPSCalculator : MonoBehaviour
    {
        // FPS Data
        private FPSSnapshot curFPSSnapshot;

        // FPS Calc Rate
        private FPSRate curFpsCalcRate;
        private float fpsCalcDelay = 0.0f;
        
        // Accumulated Frame Data
        private float accumulatedFrameTime;
        private int accumulatedFrames;
        
        private float LastCaptureTime;
        
        internal void SetUp(FPSRate fpsCalcRate)
        {
            curFpsCalcRate = fpsCalcRate;
            accumulatedFrameTime = 0.0f;
            accumulatedFrames = 0;
            LastCaptureTime = Time.unscaledTime;

            curFPSSnapshot = new FPSSnapshot();
            curFPSSnapshot.FPS = 0.0f;
            curFPSSnapshot.FrameTime = 0.0f;
            
            switch (curFpsCalcRate)
            {
                case FPSRate.EveryFrame:
                    fpsCalcDelay = -1.0f;
                    break;
                case FPSRate.Hz_1:
                    fpsCalcDelay = 1.0f / 60.0f;
                    break;
                case FPSRate.Hz_5:
                    fpsCalcDelay = 5.0f / 60.0f;
                    break;
                case FPSRate.Hz_10:
                    fpsCalcDelay = 10.0f / 60.0f;
                    break;
                case FPSRate.Hz_25:
                    fpsCalcDelay = 25.0f / 60.0f;
                    break;
                case FPSRate.Hz_60:
                    fpsCalcDelay = 60.0f / 60.0f;
                    break;
            }
        }

        internal (FPSSnapshot, bool) CalcFPS(float deltaTime)
        {
            if (curFpsCalcRate == FPSRate.EveryFrame)
            {
                float frameTime = deltaTime;
                if (frameTime == 0.0f)
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
                if (Time.unscaledTime > (LastCaptureTime + fpsCalcDelay))
                {
                    if (accumulatedFrameTime == 0.0f || accumulatedFrames == 0)
                    {
                        return (curFPSSnapshot, false);
                    }
                    
                    float fps = accumulatedFrames / accumulatedFrameTime;
                    float frameTime = accumulatedFrameTime / accumulatedFrames;
                    
                    FPSSnapshot fpsSnapshot = new FPSSnapshot
                    {
                        FPS = fps,
                        FrameTime = frameTime
                    };
                    
                    accumulatedFrameTime = 0.0f;
                    accumulatedFrames = 0;
                    LastCaptureTime = Time.unscaledTime;
                    
                    curFPSSnapshot = fpsSnapshot;
                    return (curFPSSnapshot, true);
                }
                else
                {
                    float frameTime = deltaTime;
                    accumulatedFrameTime += frameTime;
                    accumulatedFrames++;

                    return (curFPSSnapshot, false);
                }
            }
        }
    }
}