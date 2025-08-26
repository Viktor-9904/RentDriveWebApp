# Stage 1 - Build
FROM node:20 AS build
WORKDIR /app

# Copy dependencies and install
COPY package*.json ./
RUN npm install --frozen-lockfile

# Copy all source files
COPY . .

# Build the app (Vite outputs to /dist)
RUN npm run build

# Stage 2 - Serve with NGINX
FROM nginx:alpine

# Remove default nginx config
RUN rm -rf /usr/share/nginx/html/*

# Copy build output from Vite
COPY --from=build /app/dist /usr/share/nginx/html

# Copy a custom nginx.conf if you need (optional, for routing/SPAs)
# COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
