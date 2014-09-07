namespace Pineapple.Model
{
    public class Visitor
    {
        public long Id { get; set; }

        public string VisitDate { get; set; }

        public override string ToString()
        {
            return string.Format("Id={0},VisitDate={1}", Id, VisitDate);
        }
    }
}
