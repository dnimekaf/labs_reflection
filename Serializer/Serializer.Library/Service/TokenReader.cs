namespace Serializer.Library.Service
{
    internal static class TokenReader
    {
        public static string GetNextToken(string line, ref int currentPosition)
        {
            var startPosition = currentPosition;
            var quoted = line[currentPosition] == '"';

            if (quoted)
            {
                currentPosition++;
            }
            
            while (currentPosition < line.Length)
            {
                if (line[currentPosition] == ',' && !quoted)
                {
                    break;
                }

                if (line[currentPosition] == '"')
                {
                    if (currentPosition + 1 < line.Length)
                    {
                        if (line[currentPosition + 1] == '"')
                        {
                            currentPosition++;
                        }
                        else
                        {
                            quoted = false;
                        } 
                    }
                    else
                    {
                        quoted = false;   
                    }
                }
                currentPosition++;
            }

            var substr = line.Substring(startPosition, currentPosition - startPosition);
            currentPosition++;
            
            return substr;
        }
    }
}