DOCKER_USERNAME ?= gisligudm18
APPLICATION_NAME ?= cryptocopapi

build:
	docker build --tag ${DOCKER_USERNAME}/${APPLICATION_NAME} .
push:
	docker push ${DOCKER_USERNAME}/${APPLICATION_NAME}
