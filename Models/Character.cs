

namespace test_project.Models;
public class Character
{
    public int  Id { get; set;}
    public string  Name { get; set;} = "Jean";
    public int  HitPoints { get; set;} = 100;
    public int  Strenght { get; set;} = 10;
    public int  Intelligence{ get; set;} = 10;
    public int  Defense { get; set;} = 10;
    public RpgClass Class { get; set;} = RpgClass.Knight;
    public User? User {get; set;}
    public Weapon? Weapon {get; set;}
    public List<Skill>? Skills {get; set;}
    public int Fights { get; set; } = 0;
    public int Defeats { get; set; } =0;
    public int Victories { get; set; }=0;

}