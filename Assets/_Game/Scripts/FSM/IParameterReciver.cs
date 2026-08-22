namespace _Game.Scripts.FSM
{
    public interface IParameterReceiver<T>
    {
        void ApplyParameter(T parameter);
    }
}