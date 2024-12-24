using SEP.Interfaces;

namespace SEP.Observers
{
    public class DocumentDetailManager
    {
        private List<IDocumentObserver> _observers = new List<IDocumentObserver>();

        // Add an observer
        public void RegisterObserver(IDocumentObserver observer)
        {
            _observers.Add(observer);
        }

        // Remove an observer
        public void UnregisterObserver(IDocumentObserver observer)
        {
            _observers.Remove(observer);
        }

        // Notify all observers when a document is updated
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
