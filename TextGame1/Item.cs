using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame1
{

    [Flags]
    public enum eItemType
    {
        None = 0,
        Weapon = 1 << 0,
        Shield = 1 << 1,
        Helmet = 1 << 2,
        Chest = 1 << 3,
        Gauntlet = 1 << 4,
        Boots = 1 << 5,
        Ring = 1 << 6,
        Amulet = 1 << 7,

        All = int.MaxValue
    }

    [Serializable]
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
      
        public Dictionary<eStatus, float> EquipStatus { get; set; }

        public string Description { get; set; } = "";
        public bool IsEquipped { get; set; } = false;
        public eItemType ItemType { get; set; }
        
        public Item(int id, string name, int cost, eItemType itemType, Dictionary<eStatus, float> equipStatus, string description)
        {
            ID = id;
            Name = name;
            Cost = cost;
            EquipStatus = new Dictionary<eStatus, float>(equipStatus);
            Description = description;
            ItemType = itemType;
        }

        public Item(Item item)
        {
            ID = item.ID;
            Name = item.Name;
            Cost = item.Cost;
            EquipStatus = new Dictionary<eStatus, float>(item.EquipStatus);
            Description = item.Description;
            IsEquipped = item.IsEquipped;
            ItemType = item.ItemType;
        }

    }
}
