using System.Collections.Generic;

namespace MappingTests
{
    public class One
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class OneDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
    
    public class Two
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<One> Ones { get; set; }
    }
    
    public class TwoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<One> Ones { get; set; }
    }
    
    public class Three
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Two Two { get; set; }
        public One One { get; set; }
    }
    
    public class ThreeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Two Two { get; set; }
        public One One { get; set; }
    }
    
}