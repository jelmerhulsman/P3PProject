using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jan_die_alles_kan.Shortcodes
{
    public static class ShortSplitter
    {
        public static string ShortReplace(string text)
        {
            for (int i = 0; i < text.Length - 1; i++)
            {
                if ((text[i] == ']') && (text[i+1] == '*'))
                {
                    for (int ii = i - 1; ii > 0; ii--)
                    {
                        if ((text[ii] == '[') && (text[ii-1] == '*'))
                        {
                            string shortcode = text.Substring(ii+1, i - (ii+1));
                            
                            text = text.Remove((ii - 1), ((i + 2) - (ii - 1)));

                            text = text.Insert(ii-1, ShortCheck.ReturnShortText(shortcode));
                            i = ii - 1;
                            break;
                        }
                    }
                }
            }

            return text;
        }
    }
}