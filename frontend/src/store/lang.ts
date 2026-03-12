import { defineStore } from "pinia";
import type { TPage, TPageJsonContent, TranslationKey } from '@modules/types/index';

type TLang = 'ru' | 'en';

interface ILangState {
    lang: TLang,
    content: TPageJsonContent,
    isLoading: boolean
}

//перепиши getByPath и getText, так как один возвращает что угодно, а другой именно строку

const useLangStore = defineStore('lang', {
    state: (): ILangState => ({ 
        lang: (localStorage.getItem('lang') || 'en') as TLang,
        content: {} as TPageJsonContent,
        isLoading: true,
    }),
    actions: {
        //импортирует json файл страницы с учетом активного языка
        async getPageContent(page: string){
            this.isLoading = true
            try {
                const contentJson = await import(`../modules/locales/${page}/${this.lang}.json`,
                                    { with: { type: 'json' }})
                this.content = contentJson.default;
            } catch (error: unknown) {
                console.log("Lang content loading error: " + error)
            } finally {
                this.isLoading = false
            }
        },

        async setLang(page: TPage) {
            this.lang = this.lang === 'en' ? 'ru' : 'en';
            localStorage.setItem('lang', this.lang);
            await this.getPageContent(page);
        },

        //возвращает обьект или массив по переданному пути
        getText(path: TranslationKey) {
            const keys = path.split(".")
            let result: unknown = this.content

            for (const key of keys) {
                if (typeof result !== "object" || result === null) {
                    return `[${path}]`
                }
                result = (result as Record<string, unknown>)[key]
            }
            return typeof result === "string" ? result : `[${path}]`
        },

        //возвращает обьект, массив и что угодно в принципе
        getByPath<T>(path: TranslationKey): T | undefined {
            const keys = path.split('.')
            let result: unknown = this.content

            for (const key of keys) {
                if (result === null || result === undefined) {
                    return undefined
                }

                if (Array.isArray(result)) {
                    const index = Number(key)

                    if (!Number.isInteger(index)) {
                        return undefined
                    }
                    result = result[index]
                } else if (typeof result === "object") {
                    result = (result as Record<string, unknown>)[key]
                } else {
                    return undefined
                }
            }
            return result as T
        }
    }
})

export default useLangStore