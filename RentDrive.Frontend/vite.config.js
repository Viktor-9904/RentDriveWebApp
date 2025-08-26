import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import fs from 'fs'
import path from 'path'

// https://vite.dev/config/

const isDev = process.env.NODE_ENV !== 'production'

export default defineConfig({
  plugins: [react()],
  server: isDev
    ? {
        https: {
          key: fs.readFileSync('./certs/localhost-key.pem'),
          cert: fs.readFileSync('./certs/localhost.pem')
        }
      }
    : undefined,
    port: 9904,
});
