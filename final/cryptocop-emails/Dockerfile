FROM python:3.10
LABEL "maintainer"="Gisli Gudmundsson"

WORKDIR /app

COPY requirements.txt requirements.txt

RUN pip3 install -r requirements.txt

COPY . .

ENTRYPOINT [ "python3", "EmailService.py"]