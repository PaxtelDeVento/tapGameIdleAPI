using User.API.Model;

namespace UserUpgrade.API.Model
{
    public class UserUpgradeModel{
        public int Id {get;set;}
        public int UserId {get;set;}
        public int UpgradeId {get;set;}
        public int Amount {get;set;}
        public int Current_cost {get;set;}
    }
}