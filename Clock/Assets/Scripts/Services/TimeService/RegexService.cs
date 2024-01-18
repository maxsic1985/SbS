using System.Text.RegularExpressions;

namespace MSuhinin.Clock
{
    public class RegexService
    {
        
        public void Initialize(string pattern, string text)
        {
            
        }

        public bool CheckRegex(string pattern,string text)
        {
           return new Regex(@pattern).IsMatch(text);
        }
    }

  
}