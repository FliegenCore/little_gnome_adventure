using UniRx;

namespace _Game.Scripts.DialogueSystem
{
    public class DialogueModel
    {
        public readonly ReactiveProperty<bool> SkipIsEnabled = new ReactiveProperty<bool>(true);
        public readonly Subject<string> OnDialogueEnd = new  Subject<string>();
    }
}