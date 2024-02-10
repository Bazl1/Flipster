import { defineConfig } from "cypress";

export default defineConfig({
    e2e: {
        setupNodeEvents(on, config) {},
        video: false,
        baseUrl: "http://localhost:5173",
    },
});
