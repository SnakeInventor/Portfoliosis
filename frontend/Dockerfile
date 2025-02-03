FROM node AS builder
WORKDIR /app

# copy package list
COPY package.json ./
COPY package-lock.json ./

# Install packages
RUN npm install

# copy everything from the project folder
COPY . .

# build the application
RUN npm run build

# setup second stage
FROM nginx:alpine

# copy configuration template
COPY --from=builder ./app/nginx/default.conf.template /etc/nginx/templates/default.conf.template

# copy static files
COPY --from=builder ./app/dist /usr/share/nginx/portfoliosis

EXPOSE 443