using UnityEngine;
using UnityEditor;

public interface ISubject
{
	void Attach(IObserver observer);

	void Detach(IObserver observer);

	void Notify();
}