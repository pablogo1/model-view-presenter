# Master Detail With Events

This folder represents a way how MVP pattern can be implemented in a Master-Detail situation.

## Implementation details

MVP pattern implemented using events. When an action has taken place on the view (ie. the user has clicked
on the "save" button) a event is raised. This event is handled by the presenter. The presenter will execute 
the business logic and then instruct the view to display refreshed data.

In this case the dependencies are set to:

1. The view requires an instance of the presenter.
2. The view then attach/register it-self to the presenter.
3. The presenter requires only instances of the required services to satisfy the requirements.

As this is a Windows Forms application using a IoC container. We need to create an instance of 
the "Form" class wich will be used to execute the application. But doing so we end up having different 
instances when resolving the presenter (unless marking the form instance as singleton which I didn't wanted 
to do).

To be clear, is not a bad thing that the View knows about the Presenter. In fact, in other modes/examples is
necessary. As long as the responsabilities are well defined and followed. I'd have liked however if I could
keep the View completely unaware of the presenter just for sake of clarity: Have the presenter require an
instance of the view, wire up the events and then direct the view. All this without requiring the circular
dependency (I don't like that much).

## Advantages
1. Using events helps mantaining the API consistent as changes needed for more information can be passed on
2. the event arguments object instead of a method signature. Which you know, any renaming there requires
3. complete refactoring.
4. Helps maintaining the view less dependent on the presenter. The view will fire the events. The presenter
5. subscribed will act on those.

## Disadvantanges
1. Tends to be too verbose because of the Event declaration and firing. On top of that we need to listen 
for windows forms controls events to act.
2. It's hard to test since we need to provide the presenter with a mocked presenter.