export type Email = `${string}@${string}.${string}`

export interface IRegisterUser {
  username: string,
  email: Email,
  password: string,
}

export interface ILogin {
  "email": Email,
  "password": string
}