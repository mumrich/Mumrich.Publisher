import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

const protocol = process.env.HMR_PROTOCOL ?? "wss";
const port = Number(process.env.HMR_PORT) ?? 3000;

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    port: 3000,
    strictPort: true,
    hmr: {
      protocol,
      port,
    },
  },
});
