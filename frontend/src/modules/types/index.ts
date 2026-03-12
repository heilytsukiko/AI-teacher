import * as jsonPageStruct from '@modules/locales/grammar/en.json'


//тип из структуры json страницы
type TPageStruct = typeof jsonPageStruct


//соединяет ключи в строку
type Join<K, P> =
  K extends string | number
    ? P extends string | number
      ? `${K}.${P}`
      : never
    : never


//Рекурсивно проходит по структуре json
type NestedKeys<T> =
  T extends readonly (infer U)[]
    ? number | Join<number, NestedKeys<U>>
    : T extends object
      ? {
          [K in keyof T & string]:
            T[K] extends object
              ? K | Join<K, NestedKeys<T[K]>>
              : K
        }[keyof T & string]
      : never


export type TranslationKey = NestedKeys<TPageStruct>


type TPathArray = (string | number)[]




export type TPage = 'grammar';
export type TSectionsName = 'home' | 'firstSection' | 'secondSection';


interface IContentArrayTextExamples {
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