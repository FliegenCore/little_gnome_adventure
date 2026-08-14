namespace _Game.Scripts.Helpers
{
    public static class StringHelper
    {
        public static string FillParams(this string text, params object[] param)
        {
            string newText = string.Format(text, param);
            
            return newText;
        }
        
        public static string PutBrackets(this string text)
        {
            string newText = $"[{text}]";
            text =  newText;
            return text;
        }
    }
}