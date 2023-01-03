namespace WebApp.ChainOfResponsibility.ChainOfResponsibility;


public abstract class Processhandler : IProcessHandler
{
    private IProcessHandler nextProcessHandler;

    public virtual object Handle(object o)
    {
        if (nextProcessHandler != null)
        {
            return nextProcessHandler.Handle(o);
        }
        return null;
    }

    public IProcessHandler SetNext(IProcessHandler processHandler)
    {
        nextProcessHandler = processHandler;
        return nextProcessHandler;
    }
}