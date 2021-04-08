using static MontyHallSimulator.Support.IdGenerator;

namespace MontyHallSimulator.Models
{
    public record Door
    {
        public int Id { get; } = GetId();
        public DoorContent Content { get; init; }

        public Door(DoorContent content) => Content = content;
    }
}