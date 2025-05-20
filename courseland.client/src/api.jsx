

export const domain = "https://localhost:7170";
export const prefixV1 = "/api/v1";

export const API = {
    users: domain + prefixV1 + "/users",
    roles: domain + prefixV1 + "/users/roles",
    login: domain + prefixV1 + "/users/login",
    logout: domain + prefixV1 + "/users/logout",
    registration_request: domain + prefixV1 + "/registration/requests"
}

export function add_id(url, id) {
    return url + '/' + id;
}

export function add_param(url, params) {
    if (!params || Object.keys(params).length === 0) {
        return url;
    }

    const urlObj = new URL(url);

    for (const [key, value] of Object.entries(params)) {
        if (value !== undefined && value !== null) {
            urlObj.searchParams.set(key, String(value));
        }
    }

    return urlObj.toString();
}

export function add_id_and_param(url, id, params) {
    return add_param(add_id(url, id), params);
}