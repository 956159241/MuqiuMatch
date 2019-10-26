namespace DSSportCompetitionSys.Entity
{
    public class PersonInfo
    {
        // 导入时的一个序号，也是原始序号
        public int Num { get; set; }

        // 在生成对阵表时显示的一个序号
        public int MatchDisplayNum { get; set; }

        // 比赛人员的姓名
        public string Name { get; set; }
        
        // 比赛人员的性别
        public string Sex { get; set; }
        
        // 单位名称
        public string Organization { get; set; }
        
        // 组别，在数据导入时筛选使用， 例如：老年组，少年组，中年组
        public string Group { get; set; }

        // 种子号， 记录人员的种子序号
        public int SeedNum { get; set; }

        // 一下记录的是每人在每一轮的分数
        public double FirstRoundscore { get; set; }
        public double SecondRoundscore { get; set; }
        public double ThirdRoundscore { get; set; }
        public double FourthRoundscore { get; set; }
        public double FifthRoundscore { get; set; }
        public double SixRoundscore { get; set; }
    }
}