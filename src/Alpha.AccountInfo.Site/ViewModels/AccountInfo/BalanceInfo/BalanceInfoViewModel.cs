//using Alpha.AccountInfo.Models.AccountInfo;

//namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
//{
//    public class BalanceInfoViewModel
//    {
//        private readonly BalanceInfoModel model;
//        private readonly byte roleId;

//        public BalanceInfoViewModel(byte roleId, BalanceInfoModel model)
//        {
//            this.model = model;
//            this.roleId = roleId;
//            //TotalCash = totalCash;
//            //YesterdayCash = yesterdayCash;

//            //TotalBalance = totalBalance;
//            //YesterdayBalance = yesterdayBalance;

//            //Outstanding = outstanding;
//            //UsedCredit = usedCredit;
//            //GivenCredit = givenCredit;
//            //CurrentGivenCredit = currentGivenCredit;
//            //NewGivenCredit = newGivenCredit;
//            //Difference = difference;
//        }

//        public decimal TotalCash
//        {
//            get
//            {
//                switch (roleId)
//                {
//                    case 3:
//                        return model.MasterCash;
//                    case 2:
//                        return model.AgentCash;
//                    default:
//                        return model.CustomerCash;
//                }
//            }
//        }

//        public decimal YesterdayCash
//        {
//            get
//            {
//                switch (roleId)
//                {
//                    case 3:
//                        return model.Yesterday_MasterCash;
//                    case 2:
//                        return model.Yesterday_AgentCash;
//                    default:
//                        return model.Yesterday_CustomerCash;
//                }
//            }
//        }

//        public decimal TotalBalance
//        {
//            get
//            {
//                switch (roleId)
//                {
//                    case 3:
//                        return model.MasterCash + model.AgentCash + model.CustomerCash;
//                    case 2:
//                        return model.AgentCash + model.CustomerCash;
//                    default:
//                        return model.CustomerCash;
//                }
//            }
//        }

//        public decimal YesterdayBalance
//        {
//            get
//            {
//                switch (roleId)
//                {
//                    case 3:
//                        return model.Yesterday_MasterCash + model.Yesterday_AgentCash + model.Yesterday_CustomerCash;
//                    case 2:
//                        return model.Yesterday_AgentCash + model.Yesterday_CustomerCash;
//                    default:
//                        return model.Yesterday_CustomerCash;
//                }
//            }
//        }

//        public decimal Outstanding
//        {
//            get
//            {
//                switch (roleId)
//                {
//                    case 3:
//                        return model.MasterPositionTaking;
//                    case 2:
//                        return model.AgentPositionTaking;
//                    default:
//                        return model.CustomerOutstanding;
//                }
//            }
//        }

//        public decimal UsedCredit
//        {
//            get
//            {
//                switch (roleId)
//                {
//                    case 3:
//                        return model.MasterUsedCredit;
//                    default:
//                        return model.AgentUsedCredit;
//                }
//            }
//        }

//        public decimal GivenCredit { get; }

//        public decimal CurrentGivenCredit { get; }

//        public decimal NewGivenCredit { get; }

//        public decimal Difference { get; }

//        public decimal BetCredit { get; }

//        public decimal NetPostion { get; }

//        public decimal MemberMaxCredit { get; }
//    }
//}