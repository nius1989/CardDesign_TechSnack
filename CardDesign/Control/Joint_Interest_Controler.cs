using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardDesign
{
    /// <summary>
    /// Start a thread to periodically check whether two cards are highlighed by two users
    /// </summary>
    public class Joint_Interest_Controler
    {
        MainWindow mainWindow;
        Thread jointInterestDetectionThread;
        bool isRunning = false;
        DateTime[] startTimes=new DateTime[STATICS.CARD_NUMBER];
        public Joint_Interest_Controler(MainWindow mainWindow) {
            this.mainWindow = mainWindow;
        }
        public void Start()
        {
            if (isRunning)
            {
                isRunning = false;
                Thread.Sleep(100);
                jointInterestDetectionThread.Abort();
            }
            for (int i = 0; i < startTimes.Length;i++ )
            {
                startTimes[i] = DateTime.MaxValue;
            }
            jointInterestDetectionThread = new Thread(new ThreadStart(scan));
            isRunning = true;
            jointInterestDetectionThread.Start();
        
        }
        private void scan() {
            while (isRunning) {
                Thread.Sleep(5000);
                for (int cardIndex = 0; cardIndex < STATICS.CARD_NUMBER; cardIndex++) {
                    int countSame = 0;

                    if (STATICS.ALEX_ACTIVE && mainWindow.UserControler.UserList["Alex"].Cards.Count>0
                        && mainWindow.UserControler.UserList["Alex"].Cards[cardIndex].Brightness > 0.8
                        && !mainWindow.UserControler.UserList["Alex"].Cards[cardIndex].IsJointInterested)
                    {
                        countSame++;
                    }
                    if (STATICS.BEN_ACTIVE && mainWindow.UserControler.UserList["Ben"].Cards.Count > 0 
                        && mainWindow.UserControler.UserList["Ben"].Cards[cardIndex].Brightness > 0.8
                        && !mainWindow.UserControler.UserList["Ben"].Cards[cardIndex].IsJointInterested)
                    {
                        countSame++;
                    }
                    if (STATICS.CHRIS_ACTIVE && mainWindow.UserControler.UserList["Chris"].Cards.Count > 0 
                        && mainWindow.UserControler.UserList["Chris"].Cards[cardIndex].Brightness > 0.8
                        && !mainWindow.UserControler.UserList["Chris"].Cards[cardIndex].IsJointInterested)
                    {
                        countSame++;
                    }
                    if (STATICS.DANNY_ACTIVE && mainWindow.UserControler.UserList["Danny"].Cards.Count > 0 
                        && mainWindow.UserControler.UserList["Danny"].Cards[cardIndex].Brightness > 0.8
                        && !mainWindow.UserControler.UserList["Danny"].Cards[cardIndex].IsJointInterested)
                    {
                        countSame++;
                    }
                    if (countSame > 1) {
                        if (STATICS.ALEX_ACTIVE)
                            mainWindow.UserControler.UserList["Alex"].Cards[cardIndex].HightlightJointInterest();
                        if (STATICS.BEN_ACTIVE)
                            mainWindow.UserControler.UserList["Ben"].Cards[cardIndex].HightlightJointInterest();
                        if (STATICS.CHRIS_ACTIVE)
                            mainWindow.UserControler.UserList["Chris"].Cards[cardIndex].HightlightJointInterest();
                        if (STATICS.DANNY_ACTIVE)
                            mainWindow.UserControler.UserList["Danny"].Cards[cardIndex].HightlightJointInterest();
                        startTimes[cardIndex] = DateTime.Now;
                        mainWindow.ControlWindow.UpdateTextInfo("Card " +
                            mainWindow.UserControler.UserList["Alex"].Cards[cardIndex].UID + " is Joint interest @ " + 
                            startTimes[cardIndex].ToString(), 2);
                        
                    }
                }
                for (int i = 0; i < startTimes.Length;i++ )
                {
                    DateTime time = startTimes[i];
                    if (time != DateTime.MaxValue)
                    {
                        if ((DateTime.Now - time).TotalSeconds > STATICS.JOINT_INTEREST_DURATION)
                        {
                            mainWindow.ControlWindow.UpdateTextInfo("Card " + mainWindow.UserControler.UserList["Alex"].Cards[i].UID + " is time out", 2);
                            mainWindow.UserControler.UserList["Alex"].Cards[i].DehightJointInterest();
                            mainWindow.UserControler.UserList["Ben"].Cards[i].DehightJointInterest();
                            mainWindow.UserControler.UserList["Chris"].Cards[i].DehightJointInterest();
                            mainWindow.UserControler.UserList["Danny"].Cards[i].DehightJointInterest();
                            startTimes[i] = DateTime.MaxValue;
                        }
                    }
                }
            }
        }
    }
}
