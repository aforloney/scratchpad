# Running the application local

## Docker build/run commands

```text
docker build -t my_new_tag .
docker run -t my_new_tag
```

## Docker compose

With our `Dockerfile` defined locally, we can write our `docker-compose` file to reference the path without needing to build & run tags explicitly.

```text
docker-compose up
docker-compose down --remove-orphans
```

### Examples

Each written example inherits `IExample` to provide shared functionality - mainly, the core function of invoking the example and a name. When running the program locally, all of the examples will execute in parallel in no specific order.
