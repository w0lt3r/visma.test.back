using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Helper
{
    public static class StringHelper
    {
        public static string RemoveAccent(string previousText)
        {
            previousText = previousText.Replace("á", "a");
            previousText = previousText.Replace("é", "e");
            previousText = previousText.Replace("í", "i");
            previousText = previousText.Replace("ó", "o");
            previousText = previousText.Replace("ú", "u");
            return previousText;
        }
        public static string RemoveUtf8(string previousText)
        {
            previousText = previousText.Replace("\u00e1", "a");
            previousText = previousText.Replace("\u00e9", "e");
            previousText = previousText.Replace("\u00ed", "i");
            previousText = previousText.Replace("\u00f3", "o");
            previousText = previousText.Replace("\u00fa", "u");
            return previousText;
        }
    }
}
