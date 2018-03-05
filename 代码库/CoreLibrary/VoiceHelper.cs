using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;

namespace CoreLibrary
{
    public class VoiceHelper
    {
        private static readonly SpeechSynthesizer Talker = new SpeechSynthesizer();

        public static void voice(string strspeech)
        {
            Talker.Rate = 2;
            Talker.Volume = 100;
            Talker.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Child, 1, System.Globalization.CultureInfo.CurrentCulture);
            Talker.SpeakAsync(strspeech);
        }
    }
}
