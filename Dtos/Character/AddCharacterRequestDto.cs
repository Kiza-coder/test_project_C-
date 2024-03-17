namespace test_project.Dtos.Character
{
    public class AddCharacterRequestDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "Jean";
        public int HitPoints { get; set; } = 100;
        public int Strenght { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}