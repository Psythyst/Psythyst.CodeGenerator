mkdir -p ./Publish
DIRECTORY="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

DOCKER_VOLUME="$DIRECTORY/Publish:/Publish"
DOCKER_COMMAND="cp -R /Psythyst.CodeGenerator/Psythyst.Plugin.CodeGenerator.Entitas/Psythyst.Plugin.CodeGenerator.Entitas/Psythyst.Plugin.CodeGenerator.Entitas/Publish /"
DOCKER_IMAGE="psythyst/psythyst-code-generator:latest"

docker run -it --rm -v $DOCKER_VOLUME $DOCKER_IMAGE $DOCKER_COMMAND