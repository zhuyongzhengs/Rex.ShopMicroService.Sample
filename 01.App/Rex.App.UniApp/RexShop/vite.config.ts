import { defineConfig } from "vite";
import uni from "@dcloudio/vite-plugin-uni";
import autoImport from 'unplugin-auto-import/vite';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    uni(),
    autoImport({ // 使用
      imports: ['vue'],
      dts: 'src/auto-import.d.ts'
    })
  ],
});
