declare module "*.css";
declare module "*.svg";
declare module "*.png";
declare module "*.jpg";
declare module "*.jpeg";
declare module "*.gif";
declare module "*.webp";

declare namespace NodeJS {
  interface Global {
    fetch: jest.Mock;
    import: {
      meta: {
        glob: (path: string) => Record<string, { default: string }>;
      };
    };
  }
}

interface Window {
  matchMedia: jest.Mock;
}