/* eslint-disable */
// TypeScript declaration file for tests

// Declare Jest global types
declare namespace jest {
  interface Matchers<R> {
    toBeInTheDocument(): R;
    toHaveAttribute(attr: string, value?: string): R;
  }
  function fn<T extends (...args: any[]) => any>(implementation?: T): jest.MockInstance<ReturnType<T>, Parameters<T>>;
}

interface MockInstance<T, Y extends any[]> {
  new (...args: Y): T;
  (...args: Y): T;
  mockClear(): void;
  mockReset(): void;
  mockImplementation(fn?: (...args: Y) => T): MockInstance<T, Y>;
  mockImplementationOnce(fn?: (...args: Y) => T): MockInstance<T, Y>;
  mockReturnThis(): MockInstance<T, Y>;
  mockReturnValue(val: T): MockInstance<T, Y>;
  mockReturnValueOnce(val: T): MockInstance<T, Y>;
}

// Declare global test functions
declare function describe(name: string, fn: () => void): void;
declare function test(name: string, fn: () => void): void;
declare function it(name: string, fn: () => void): void;
declare function expect<T>(actual: T): jest.Matchers<void>;
declare function beforeEach(fn: () => void): void;
declare function afterEach(fn: () => void): void;
declare function beforeAll(fn: () => void): void;
declare function afterAll(fn: () => void): void;