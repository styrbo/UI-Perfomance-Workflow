namespace UIWorkflow.Events
{
    public interface IEnableEvent
    {
        void OnEnableUI();
    }

    public interface IDisableEvent
    {
        void OnDisableUI();
    }

    public interface IActiveEvents : IEnableEvent, IDisableEvent
    {
        
    }
}