using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jan_die_alles_kan.Shortcodes
{
    public static class ShortSplitter
    {
        /// <summary>
        /// This function goes through the content of a page and searches for the following pattern: *[text]*
        /// If this pattern is found it will send the text within the stars and brackets to another function which
        /// checks the code and returns the text it should be changed into. Ultimately, this function returns the changed content
        /// of the page.
        /// </summary>
        /// <param name="text">The page content to check for shortcodes</param>
        /// <returns>The changed page content</returns>
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