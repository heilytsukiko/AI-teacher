import * as jsonPageStruct from '@modules/locales/grammar/en.json'


//тип из структуры json страницы
type TPageStruct = typeof jsonPageStruct


//соединяет ключи в строку
type TJoin<K, P> =
  K extends string | number
    ? P extends string | number
      ? `${K}.${P}`
      : never
    : never


//Рекурсивно проходит по структуре json
type TNestedKeys<T> =
  T extends readonly (infer U)[]
    ? number | TJoin<number, TNestedKeys<U>>
    : T extends object
      ? {
          [K in keyof T & string]:
            T[K] extends object
              ? K | TJoin<K, TNestedKeys<T[K]>>
              : K
        }[keyof T & string]
      : never


export type TTranslationKey = TNestedKeys<TPageStruct>


export type TPage = 'grammar';
export type THeader = 'header';
export type TSectionsName = 'home' | 'firstSection' | 'secondSection';


export interface IContentArrayTextExamples {
    text: string,
    examples: string[]
}


interface IContentArray {
    title: string,
    description: string,
    listDescription: string,
    content: IContentArrayTextExamples[]
}


export interface ISection {
    title: string,
    description: string,
    content?: IContentArray[] | string[]
}


export type TPageJsonContent = Record<TSectionsName, ISection>;