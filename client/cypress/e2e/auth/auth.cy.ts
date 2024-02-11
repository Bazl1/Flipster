import { API_URL } from "../../../src/shared/axios";

describe("Create account", () => {
    beforeEach(() => {
        cy.visit("/registration");
    });

    it("Visits the registration page", () => {
        cy.url().should("include", "/registration");
    });

    it("Checking for registration fields", () => {
        cy.get('[data-cy="input-name"]').should("exist");
        cy.get('[data-cy="input-email"]').should("exist");
        cy.get('[data-cy="input-password"]').should("exist");
        cy.get('[data-cy="input-confirm-password"]').should("exist");
        cy.get('[data-cy="submit"]').should("exist");

        cy.get('[data-cy="input-name"]').should("have.value", "");
        cy.get('[data-cy="input-email"]').should("have.value", "");
        cy.get('[data-cy="input-password"]').should("have.value", "");
        cy.get('[data-cy="input-confirm-password"]').should("have.value", "");
        cy.get('[data-cy="submit"]');
    });

    it("Entering test data into the form", () => {
        cy.get('[data-cy="input-name"]').type("TestName");
        cy.get('[data-cy="input-email"]').type("test@gmail.com");
        cy.get('[data-cy="input-password"]').type("qwerty123");
        cy.get('[data-cy="input-confirm-password"]').type("qwerty123");
    });

    it("Checking for a mandatory password match", () => {
        cy.get('[data-cy="input-name"]').type("TestName");
        cy.get('[data-cy="input-email"]').type("test@gmail.com");
        cy.get('[data-cy="input-password"]').type("qwerty123");
        cy.get('[data-cy="input-confirm-password"]').type("qwerty124");
        cy.get('[data-cy="submit"]').click();

        cy.contains("Password mismatch").should("exist");
    });

    it("Successful registration", () => {
        cy.intercept("POST", `${API_URL}/auth/register`).as("registerRequest");

        cy.get('[data-cy="input-name"]').type("TestName");
        cy.get('[data-cy="input-email"]').type("test@gmail.com");
        cy.get('[data-cy="input-password"]').type("qwerty123");
        cy.get('[data-cy="input-confirm-password"]').type("qwerty123");
        cy.get('[data-cy="submit"]').click();

        cy.wait("@registerRequest").then((interception) => {
            expect(interception.request.method).to.equal("POST");
            expect(interception.request.url).to.include(
                `${API_URL}/auth/register`,
            );
            expect(interception.response?.statusCode.toString()).to.equal(
                "200",
            );
        });

        cy.url().should("include", "/");
    });
});

describe("Login", () => {
    beforeEach(() => {
        cy.visit("/login");
    });

    it("Visits the login page", () => {
        cy.url().should("include", "/login");
    });

    it("Checking for authorization fields", () => {
        cy.get('[data-cy="input-email"]').should("exist");
        cy.get('[data-cy="input-password"]').should("exist");
        cy.get('[data-cy="submit"]').should("exist");

        cy.get('[data-cy="input-email"]').should("have.value", "");
        cy.get('[data-cy="input-password"]').should("have.value", "");
        cy.get('[data-cy="submit"]');
    });

    it("Entering test data into the form", () => {
        cy.get('[data-cy="input-email"]').type("test@gmail.com");
        cy.get('[data-cy="input-password"]').type("qwerty123");
    });

    it("Verification of existing data", () => {
        cy.get('[data-cy="input-email"]').type("test1@gmail.com");
        cy.get('[data-cy="input-password"]').type("qwerty1233");
        cy.get('[data-cy="submit"]').click();

        cy.contains("Incorrect email or password").should("exist");
    });

    it("Successful auth", () => {
        cy.intercept("POST", `${API_URL}/auth/login`).as("loginRequest");

        cy.get('[data-cy="input-email"]').type("test@gmail.com");
        cy.get('[data-cy="input-password"]').type("qwerty123");
        cy.get('[data-cy="submit"]').click();

        cy.wait("@loginRequest").then((interception) => {
            expect(interception.request.method).to.equal("POST");
            expect(interception.request.url).to.include(
                `${API_URL}/auth/login`,
            );
            expect(interception.response?.statusCode.toString()).to.equal(
                "200",
            );
        });

        cy.url().should("include", "/");
    });
});

describe("Logout", () => {
    beforeEach(() => {
        cy.visit("/");
    });

    it("Visits the home page", () => {
        cy.url().should("include", "/");
    });

    it("Check if there is a logout button", () => {
        cy.visit("/login");
        cy.intercept("POST", `${API_URL}/auth/login`).as("loginRequest");

        cy.get('[data-cy="input-email"]').type("test@gmail.com");
        cy.get('[data-cy="input-password"]').type("qwerty123");
        cy.get('[data-cy="submit"]').click();

        cy.wait("@loginRequest").then((interception) => {
            expect(interception.request.method).to.equal("POST");
            expect(interception.request.url).to.include(
                `${API_URL}/auth/login`,
            );
            expect(interception.response?.statusCode.toString()).to.equal(
                "200",
            );
        });

        cy.url().should("include", "/");

        cy.get('[data-cy="button-logout"]').should("exist");
    });

    it("Logging out of the account", () => {
        cy.visit("/login");
        cy.intercept("POST", `${API_URL}/auth/login`).as("loginRequest");

        cy.get('[data-cy="input-email"]').type("test@gmail.com");
        cy.get('[data-cy="input-password"]').type("qwerty123");
        cy.get('[data-cy="submit"]').click();

        cy.wait("@loginRequest").then((interception) => {
            expect(interception.request.method).to.equal("POST");
            expect(interception.request.url).to.include(
                `${API_URL}/auth/login`,
            );
            expect(interception.response?.statusCode.toString()).to.equal(
                "200",
            );
        });

        cy.url().should("include", "/");

        cy.get('[data-cy="button-logout"]').click();
        cy.get('[data-cy="button-logout"]').should("not.exist");
    });
});
