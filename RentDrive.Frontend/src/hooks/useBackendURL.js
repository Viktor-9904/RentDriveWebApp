export function useBackendURL() {
    return import.meta.env.MODE === "development" 
    ? "https://localhost:7299" 
    : "https://rentdrive.eu";
}