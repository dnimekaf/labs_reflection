namespace Serializer.Tests.Assets
{
    public class F
    {
        int i1, i2, i3, i4, i5; 
        
        public F Get() => new F()
        {
            i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5
        };

        // for testing purposes
        public override string ToString()
        {
            return $"{i1}{i2}{i3}{i4}{i5}";
        }
    }
}