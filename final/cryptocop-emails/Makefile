DOCKER_USERNAME ?= gisligudm18
APPLICATION_NAME ?= emailservice

build:
	docker build --tag ${DOCKER_USERNAME}/${APPLICATION_NAME}:latest -f Dockerfile .
push:
	docker push ${DOCKER_USERNAME}/${APPLICATION_NAME}
