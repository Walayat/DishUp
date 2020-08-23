using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DishUp.Models
{

    public class Rootobject
    {
        public Addressresult addressResult { get; set; }
    }

    public class Addressresult
    {
        public Address[] addresses { get; set; }
        public object errMsg { get; set; }
        public int numRecs { get; set; }
        public Searchmethod searchMethod { get; set; }
    }

    public class Searchmethod
    {
        public string[] methodDescriptions { get; set; }
    }

    public class Address
    {
        public int houseNumberFirst { get; set; }
        public object houseNumberFirstSuffix { get; set; }
        public int houseNumberSecond { get; set; }
        public object houseNumberSecondSuffix { get; set; }
        public int objectId { get; set; }
        public int propid { get; set; }
        public int postCode { get; set; }
        public string roadName { get; set; }
        public object roadSuffix { get; set; }
        public string roadType { get; set; }
        public string shortAddressString { get; set; }
        public string suburbName { get; set; }
        public string addressType { get; set; }
        public string council { get; set; }
        public string addressString { get; set; }
        public string houseNumberString { get; set; }
        public Addresspoint addressPoint { get; set; }
    }

    public class Addresspoint
    {
        public object addressPointType { get; set; }
        public object addressPointUncertainty { get; set; }
        public int addressstringOid { get; set; }
        public object containment { get; set; }
        public object createDate { get; set; }
        public object gurasId { get; set; }
        public object objectId { get; set; }
        public object prowayOid { get; set; }
        public object waypointOid { get; set; }
        public string coordRefSys { get; set; }
        public float centreX { get; set; }
        public float centreY { get; set; }
    }

}