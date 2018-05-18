using System.Collections.Generic;

namespace CustomT {
    public class CustomT {
        public string Name;
        public CustomTDef Type;
        public List<CustomT> Children;
    }

    public enum CustomTDef {
        number,
        text,
        boolean,
        tObject
    }
}