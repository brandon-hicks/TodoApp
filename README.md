# ToDoApp
This is an app to track things that need to be done.

This is how you add a todo. This uses a post route the information that is needed to be able to create a new todo is Id, title, and content. This will also set the completion status to false by default.

```http://localhost:5000/api/todos```
```
{
  "Internal Id": 12354,
  "Id": 0,
  "Title": "Go Home",
  "content": "Go Home"
}
```

This is how to get todos. This uses a get route and gets all todos

This is the route.

```http://localhost:5000/api/todos```

This is the response that you will get. This will return all todos.

```
[
  {
    "internalId": {
      "timestamp": 1583635925,
      "machine": 7500423,
      "pid": 3261,
      "increment": 14821626,
      "creationTime": "2020-03-08T02:52:05Z"
    },
    "id": 1,
    "title": "Test",
    "content": "This is a test to see if this works",
    "isComplete": false
  },
  {
    "internalId": {
      "timestamp": 1583635977,
      "machine": 7500423,
      "pid": 3261,
      "increment": 14821627,
      "creationTime": "2020-03-08T02:52:57Z"
    },
    "id": 2,
    "title": "do homework",
    "content": "do all homework",
    "isComplete": false
  },
  {
    "internalId": {
      "timestamp": 1583636010,
      "machine": 7500423,
      "pid": 3261,
      "increment": 14821628,
      "creationTime": "2020-03-08T02:53:30Z"
    },
    "id": 3,
    "title": "walk dog",
    "content": "walk the dog around the yard.",
    "isComplete": false
  },
  {
    "internalId": {
      "timestamp": 1583636049,
      "machine": 7500423,
      "pid": 3261,
      "increment": 14821629,
      "creationTime": "2020-03-08T02:54:09Z"
    },
    "id": 4,
    "title": "build table",
    "content": "build new kitchen table.",
    "isComplete": false
  }
]
```

To only get one todo back this is the route. This uses a get route that uses the id to search for a specific todo.

```http://localhost:5000/api/todos/1```

This would be your response.

```
{
  "internalId": {
    "timestamp": 1583635925,
    "machine": 7500423,
    "pid": 3261,
    "increment": 14821626,
    "creationTime": "2020-03-08T02:52:05Z"
  },
  "id": 1,
  "title": "Test",
  "content": "This is a test to see if this works",
  "isComplete": false
}
  ```
This is how you would delete a ToDo. This uses a delete route.

```http://localhost:5000/api/todos/3```

The response that will be given will be 

```204 No Content ```

If the delete is not successful then the response will be 

```404 Not Found```

This is how you would update the completion status of a todo this route uses a patch.

```http://localhost:5000/api/todos/1/completionStatus```

If the update is done properly the response that you will get is this

```
{
  "internalId": {
    "timestamp": 1583635925,
    "machine": 7500423,
    "pid": 3261,
    "increment": 14821626,
    "creationTime": "2020-03-08T02:52:05Z"
  },
  "id": 1,
  "title": "Test",
  "content": "This is a test to see if this works",
  "isComplete": true
}
```
If this is successful when a get todo is performed again the response will look like this 

```
{
  "internalId": {
    "timestamp": 1583635925,
    "machine": 7500423,
    "pid": 3261,
    "increment": 14821626,
    "creationTime": "2020-03-08T02:52:05Z"
  },
  "id": 1,
  "title": "Test",
  "content": "This is a test to see if this works",
  "isComplete": true
}
```

If the update is not done properly the response will be

```NoResultFound```