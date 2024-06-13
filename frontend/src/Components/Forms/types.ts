export type Action = "Log In" | "Sign Up";

export type Field = {
  title: string;
  placeholder: string;
  type: string;
  id: string;
};

export interface LoginFormSchema {
  "form-email": string;
  "form-password-login": string;
}

export interface RegisterFormSchema {
  "form-email": string;
  "form-password-register": string;
  "form-password-confirm": string;
}

export type FormSchema = LoginFormSchema | RegisterFormSchema;
