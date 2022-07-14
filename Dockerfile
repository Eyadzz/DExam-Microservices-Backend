# base image
FROM jhonatans01/python-dlib-opencv
# setup environment variable
#ENV DockerHOME=/home/app/webapp

# set work directory
#RUN mkdir -p $DockerHOME

# where your code lives
#WORKDIR $DockerHOME

# set environment variables
ENV PYTHONDONTWRITEBYTECODE 1
ENV PYTHONUNBUFFERED 1

# install dependencies

# copy whole project to your docker home directory.
COPY . .
# run this command to install all dependencies
RUN pip3 install -r requirements.txt
# port where the Django app runs
#EXPOSE 8000
# start server
CMD python3 manage.py runserver 0.0.0.0:8001
