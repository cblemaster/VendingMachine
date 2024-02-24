namespace VendingMachine.UI;

public interface IBasicUserInterface
{
    void Output(string message);
    void PauseOutput();
    object PromptForSelection(object[] options);
}