namespace Assets.Scripts.Model
{
    public interface ICommand
    {
        Vector2Integer SquareClicked(); 
        void Do();
        void Undo();
    }
}