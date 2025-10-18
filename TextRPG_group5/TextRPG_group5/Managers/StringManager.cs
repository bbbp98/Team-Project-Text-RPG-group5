using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Managers
{
     internal class StringManager
     {
          static private StringManager? instance;

          public static StringManager Instance
          {
               get
               {
                    if (instance == null)
                         instance = new StringManager();
                    return instance;
               }
          }

          public int GetPrintableLength(string str) // 문자열의 출력 길이를 계산하는 메서드
          {
               int length = 0;
               foreach (char c in str)
               {
                    if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                    {
                         length += 2; // 한글은 2칸
                    }
                    else
                    {
                         length += 1; // 그 외 1칸
                    }
               }
               return length;
          }

          public string PadRightForMixedText(string str, int totalLength) // 주어진 길이에 맞춰서 문자열 우측에 공백을 추가해주는 메서드
          {
               int currentLength = GetPrintableLength(str);
               int padding = totalLength - currentLength;
               return str.PadRight(str.Length + (padding > 0 ? padding : 0));
          }
     }
}
