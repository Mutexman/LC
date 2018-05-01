using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LC
{
    class LCTreeNodeNoList : LCTreeNodeGroup
    {
        public LCTreeNodeNoList() : base()
        {
            this.lcObjectType = LCObjectType.NoList;
        }
        public override void Save(XmlTextWriter xw)
        {
            xw.WriteStartElement("NoList");
            xw.WriteAttributeString("NameGroup", this.Text);
            xw.WriteAttributeString("Description", this.Description);
            xw.WriteAttributeString("FotoFile", this.fotoFile);
        }
    }
}