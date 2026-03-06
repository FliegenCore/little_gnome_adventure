namespace _Game.Scripts.InteractionSystems
{
    public class ContactTrigger
    {
        public readonly IContactTriggerProvider ContactTriggerProvider;
        
        public ContactTrigger(IContactTriggerProvider contactTriggerProvider)
        {
            ContactTriggerProvider = contactTriggerProvider;
        }
    }
}